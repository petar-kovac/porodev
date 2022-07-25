export interface IFilesRequest {
  name: string;
  email: string;
}

export interface IProfileRequest {
  avatarUrl: string;
  department: number;
  email: string;
  name: string;
  lastname: string;
  passwordUnhashed: string;
  position: string;
  role: number;
}
