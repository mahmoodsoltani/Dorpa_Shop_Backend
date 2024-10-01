export interface ColorReadDto extends BaseReadDto<Color> {
    name: string;
    code: string;
}

export interface ColorCreateDto {
    name: string;
    code: string;
}

export interface ColorUpdateDto {
    id: number;
    name: string | null;
    code: string | null;
}

export interface ColorUpdateValidator {

}

export interface ColorCreateValidator {

}