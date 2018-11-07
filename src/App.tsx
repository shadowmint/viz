import React, {Component} from "react";
import "../node_modules/react-vis/dist/style.css";
import "./App.css";
import {SampleViz} from "./samples/sample-viz/sampleViz";

export interface IApp {
    backend: string;
}

class App extends Component<IApp> {
    public render() {
        return (
            <div className="App">
                <SampleViz backend={this.props.backend} interval={3000}/>
            </div>
        );
    }
}

export default App;
