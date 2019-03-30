import * as React from "react";
import {
    FlexibleWidthXYPlot,
    HorizontalGridLines, LineSeries,
    VerticalGridLines,
    XAxis,
    YAxis,
} from "react-vis";

export interface ISampleData {
    data: number[];
}

export function SampleData(props: ISampleData) {
    return (
        <>
            <FlexibleWidthXYPlot height={300}>
                <VerticalGridLines/>
                <HorizontalGridLines/>
                <XAxis/>
                <YAxis/>
                <LineSeries data={
                    props.data.map((i, v) => {
                        return {x: v, y: i};
                    })
                }/>
            </FlexibleWidthXYPlot>
        </>
    );
}
