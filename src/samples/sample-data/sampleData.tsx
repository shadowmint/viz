import * as React from "react";
import {
    HorizontalGridLines,
    MarkSeries, MarkSeriesPoint,
    VerticalGridLines,
    XAxis, XYPlot,
    YAxis
} from "react-vis";

export interface ISampleData {
    data: number[];
}


export class SampleData extends React.PureComponent<ISampleData> {
    public render() {
        const data = this.reduceData();
        return (
            <div>
                Here's a graph:
                <div>
                    <XYPlot height={300} width={300}>
                        <VerticalGridLines/>
                        <HorizontalGridLines/>
                        <XAxis/>
                        <YAxis/>
                        <MarkSeries data={data}/>
                    </XYPlot>
                </div>
            </div>

        );
    }

    private reduceData(): MarkSeriesPoint[] {
        const data = this.props.data;
        const result = [];
        for (let i = 0; i < data.length / 3; i += 3) {
            result.push({x: data[i] * 10, y: data[i + 1] * 10, size: data[i + 2] * 10});
        }
        return result;
    }
}
