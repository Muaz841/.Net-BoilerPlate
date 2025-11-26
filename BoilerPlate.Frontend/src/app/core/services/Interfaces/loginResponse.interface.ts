export interface LoginResponse {
  accessToken: string;
  user: {
    id: string;
    userName: string;
    name: string;
    surname: string;
    email: string;
    roles: string[];
    permissions: string[];
  };
}