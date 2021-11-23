export interface Order {
	id: number;
	totalCost: number;
	orderDate: Date;
	orderDetials: OrderDetial[];
}

export interface OrderDetial {
	itemName: string;
	quantity: number;
	unitPrice: number;
}