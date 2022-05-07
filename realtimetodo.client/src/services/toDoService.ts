import Vue from "vue";
import { EventEmitter } from "events";
import { PluginObject, VueConstructor } from "vue/types/umd";

import { LogLevel, HubConnectionBuilder, HubConnectionState } from "@microsoft/signalr";

export default class ToDoService {
   
    connection: signalR.HubConnection;
    events: EventEmitter;

    constructor() {
        this.events = new EventEmitter();
        this.connection = new HubConnectionBuilder()
            .configureLogging(LogLevel.Trace)
            .withAutomaticReconnect()
            .withUrl("/hubs/todo")
            .build();

            this.connection.on("updatedToDoList", (values: any[]) => this.events.emit("updatedToDoList", values));
    }

    async start() {
       await this.connection.start();
    }

     getLists() {
       if (this.connection.state === HubConnectionState.Connected) {
        const results = this.connection.send("GetLists");

        return results;
       }
       else {
        setTimeout(() => this.getLists(), 500);
       }
    }

    getListData(id: number) {

    }

    subscribeToListUpdates(id: number) {

    }

    unsubscribeFromListUpdates(id: number) {
        
    }

    addToDoItem(listId: number, text: string) {

    }

    toggleToDoItem(listId: number, itemId: number) {

    }
}

export const ConnectionServices: PluginObject<any> = {
    install(Vue: VueConstructor<Vue>, option: any | undefined) {
        Vue.$connectionService = new ToDoService();
        Vue.prototype.$connectionService = Vue.$connectionService;
    }
}

