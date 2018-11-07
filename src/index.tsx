import React from "react";
import ReactDOM from "react-dom";
import App, {IApp} from "./App";
import "./index.css";
import AjaxFetch from "./services/ajaxFetch";
import * as serviceWorker from "./serviceWorker";

new AjaxFetch("").get <IApp>("config.json").then((settings: IApp) => {
    ReactDOM.render(<App {...settings} />, document.getElementById("root"));

    // If you want your app to work offline and load faster, you can change
    // unregister() to register() below. Note this comes with some pitfalls.
    // Learn more about service workers: http://bit.ly/CRA-PWA
    serviceWorker.unregister();
});
