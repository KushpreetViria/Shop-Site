import { Item } from "./item";

export interface Cart {
	count: number;
	dateCreated: Date;
	items: Item[];
}