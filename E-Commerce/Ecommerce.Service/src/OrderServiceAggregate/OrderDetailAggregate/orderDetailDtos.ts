export interface OrderDetailReadDto extends BaseReadDto<OrderDetail> {
    orderId: number;
    productName: string;
    price: number;
    quantity: number;
}

export interface OrderDetailCreateDto {
    orderId: number;
    productVariantId: number;
    price: number;
    quantity: number;
}

export interface OrderDetailUpdateDto {
    quantity: number;
    id: number;
}

export interface OrderDetailUpdateValidator {

}

export interface OrderDetailCreateValidator {

}