export interface IRuntimeRequest {
  fileID: string;
  jwt: string | null;
}
export interface IRuntimeRsponse {
  exceptionHappened: boolean;
  executionOutput: string;
  executionStart: string;
  executionTime: string;
}
