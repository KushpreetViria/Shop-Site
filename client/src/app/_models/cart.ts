import { Item } from "./item";

export interface Cart {
	totalCost: number;
	count: number;
	dateCreated: Date;
	items: Item[];
}