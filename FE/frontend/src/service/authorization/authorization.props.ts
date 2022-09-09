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
  role: string;
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

export interface IFindUserByIdRequest {
  id: string;
}

export interface IFindUserByIdResponse {
  id: string;
  name: string;
  lastname: string;
  email: string;
  role: number;
  department: number;
  position: string;
  avatarUrl: string;
  fileDownloadTotal: number;
  fileUploadTotal: number;
  runtimeTotal: number;
  verificationToken: string;
  verifiedAt: string;
}
