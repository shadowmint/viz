import * as React from "react";
import {Dispatch, SetStateAction, useEffect, useState} from "react";
import AjaxFetch from "../../services/ajaxFetch";
import {SampleData} from "../sample-data/sampleData";
import styles from "./sampleViz.module.css";

const SAMPLE_SIZE = 40;

export interface ISampleViz {
    backend: string;
    interval: number;
}

interface IApiResponse {
    data: number[];
}

export interface ISampleVizState {
    data: number[];
    timestamp: number;
}

export function SampleViz(props: ISampleViz) {
    const [dataset, setDataset] = useState({
        data: Array(SAMPLE_SIZE).fill(0),
        timestamp: Date.now(),
    } as ISampleVizState);

    // Setup dataset fetch
    useEffect(() => {
        const binding = setInterval(() => updateData(props.backend, setDataset), props.interval);

        // On unmount, cleanup
        return () => {
            clearTimeout(binding);
        };
    }, []);

    return (
        <div className="sampleViz">
            <div className={styles.timestamp}>
                {new Date(dataset.timestamp).toString()}
            </div>
            <div className={styles.graph}>
                <SampleData data={dataset.data}/>
            </div>
        </div>
    );
}

async function updateData(backend: string, setDataset: Dispatch<SetStateAction<ISampleVizState>>) {
    const fetcher = new AjaxFetch(backend);
    const response = await fetcher.post<IApiResponse>("/api/data", {});

    // Map prev state to next state
    setDataset((prev: ISampleVizState) => {
        const nextFrame = [...prev.data, response.data[0]];
        if (nextFrame.length > SAMPLE_SIZE) {
            nextFrame.shift();
        }
        return {
            data: nextFrame,
            timestamp: Date.now(),
        };
    });
}
