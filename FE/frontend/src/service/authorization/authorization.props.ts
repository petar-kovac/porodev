export interface ILoginRequest {
  email: string;
  password: string;
}
export interface ILoginResponse {
  name: string;
  lastname: string;
  email: string;
  password: string;
  department: number;
  position: string;
  avatarUrl: string;
  jwt: string;
}

export interface IRegisterRequest {
  name: string;
  lastname: string;
  email: string;
  password: string;
  department: number;
  position: string;
  avatarUrl: string;
}
export interface IRegisterResponse {
  name: string;
  lastname: string;
  email: string;
  password: string;
  department: number;
  position: string;
  avatarUrl: string;
}
