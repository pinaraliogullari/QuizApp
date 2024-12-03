export class RequestParameters {
    constructor(controller, action, queryString, headers = {}, baseUrl, fullEndPoint) {
        this.controller = controller;
        this.action = action;
        this.queryString = queryString;
        this.headers = headers;
        this.baseUrl = baseUrl;
        this.fullEndPoint = fullEndPoint;
    }
}