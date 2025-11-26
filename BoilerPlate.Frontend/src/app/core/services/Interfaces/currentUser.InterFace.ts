export interface CurrentUser {
  id: string;
  userName: string;
  name: string;
  surname: string;
  email: string;
  roles: string[];
  permissions: string[];
}