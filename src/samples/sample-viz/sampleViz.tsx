import * as React from "react";
import AjaxFetch from "../../services/ajaxFetch";
import Logger from "../../services/logger";
import {SampleData} from "../sample-data/sampleData";

export interface ISampleViz {
    backend: string;
    interval: number;
}

export interface ISampleVizState {
    data: number[];
    timestamp: number;
}

export class SampleViz extends React.Component<ISampleViz, ISampleVizState> {
    private logger: Logger;
    private binding: any | null = null;

    constructor(props: ISampleViz) {
        super(props);
        this.logger = new Logger();
        this.state = {
            data: [],
            timestamp: Date.now(),
        };
    }

    public componentDidMount() {
        this.binding = setInterval(() => this.updateData(), this.props.interval);
        this.updateData();
    }

    public componentWillUnmount() {
        if (this.binding) {
            clearTimeout(this.binding);
        }
    }

    public render() {
        return (
            <div className="sampleViz">
                <div>
                    (last snapshot: {new Date(this.state.timestamp).toString()})
                </div>
                <SampleData data={this.state.data}/>
            </div>
        );
    }

    private async updateData() {
        const fetcher = new AjaxFetch(this.props.backend);
        const data = await fetcher.post("/api/data", {});
        this.setState({timestamp: Date.now(), ...data});
    }
}
