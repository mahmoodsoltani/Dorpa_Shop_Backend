export interface LoginResponse {
    token: string;
    email: string;
    firstName: string;
    lastName: string;
    is_Admin: boolean;
}