export interface CategoryReadDto extends BaseReadDto<Category> {
    name: string;
    description: string;
    parentId: number | null;
    parentName: string | null;
}

export interface CategoryCreateDto {
    name: string;
    description: string;
    parentId: number | null;
}

export interface CategoryUpdateDto {
    id: number;
    name: string | null;
    description: string | null;
    parentId: number | null;
}

export interface CategoryUpdateValidator {

}

export interface CategoryCreateValidator {

}