import "whatwg-fetch";
import Logger from "./logger";

export default class AjaxFetch {
    private logger: Logger;

    constructor(private rootUrl: string) {
        this.logger = new Logger();
    }

    public async get<T>(url: string): Promise<T> {
        const response = await fetch(`${this.rootUrl}${url}`, {method: "GET"});
        if (!response.ok) {
            throw new Error(response.statusText);
        }
        return response.json();
    }

    public async post<T>(url: string, body: any): Promise<T> {
        const apiHeaders = new Headers({
            "Accept": "*/*",
            "Content-Type": "application/json",
            "X-Requested-With": "SERVICE",
        });

        const raw = this.serializeRequestBody(body);
        const response = await fetch(`${this.rootUrl}${url}`, {
            body: raw,
            cache: "no-cache",
            credentials: "include",
            headers: apiHeaders,
            method: "POST",
            mode: "cors",
            redirect: "follow",
        });

        if (!response.ok) {
            this.logger.error(response);
            throw new Error("Request failed");
        }

        return await response.json();
    }

    private serializeRequestBody(body: any): string {
        try {
            return JSON.stringify(body);
        } catch (error) {
            this.logger.info("Unable to serialize object", body);
            this.logger.error(error);
            throw new Error("Request failed: Unable to serialize object for POST");
        }
    }
}
