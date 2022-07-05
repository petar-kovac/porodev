export interface IRuntimeRequest {
  projectId: string;
  arguments: string[];
}
export interface IRuntimeRsponse {
  exceptionHappened: boolean;
  executionOutput: string;
  executionStart: string;
  executionTime: number;
}
