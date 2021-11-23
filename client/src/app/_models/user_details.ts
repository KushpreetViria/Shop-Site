import { Cart } from "./cart";
import { Order } from "./order";

export interface UserDetails {
    id: number;
    username: string;
    fullAddress?: any;
    address?: any;
    city?: string;
    state?: string;
    country?: string;
    postalCode?: string;
    dateOfBirth?: Date;
    firstName?: string;
    lastName?: string;
    email?: string;
    orders: Order[];
    dateCreated: Date;
    cart?: Cart;
}