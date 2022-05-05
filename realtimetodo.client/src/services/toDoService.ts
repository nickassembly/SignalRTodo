import Vue, { PluginObject } from "vue";
import { VueConstructor } from "vue";

import { LogLevel, HubConnectionBuilder } from "@microsoft/signalr";

export default class ToDoService {
   
    connection: signalR.HubConnection;

    constructor() {
        this.connection = new HubConnectionBuilder()
            .configureLogging(LogLevel.Trace)
            .withAutomaticReconnect()
            .withUrl("/hubs/todo")
            .build();
    }

    async start() {
       await this.connection.start();
    }

    async getLists() {
        // TODO: check to make sure your status is good and you are connected to the hub
        const results = await this.connection.invoke("GetLists");

        return results;
    }
}

export const ConnectionServices: PluginObject<any> = {
    install(Vue: VueConstructor<Vue>, option: any | undefined) {
        Vue.$connectionService = new ToDoService();
        Vue.prototype.$connectionService = Vue.$connectionService;
    }
}

