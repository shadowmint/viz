import React from "react";
import "../node_modules/react-vis/dist/style.css";
import "./App.css";
import {SampleViz} from "./samples/sample-viz/sampleViz";

export interface IApp {
    backend: string;
}

export function App(props: IApp) {
    return (
        <div className="App">
            <SampleViz backend={props.backend} interval={100}/>
        </div>
    );
}

export default App;
