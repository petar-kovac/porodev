import api from '../base';
import { IRuntimeRequest, IRuntimeRsponse } from './runtime.props';

export const getRuntime: () => Promise<string> = () => {
  return api.service().get('/api/Runtime/ExecuteProject');
};

export const startRuntimeService: (
  payload: IRuntimeRequest,
) => Promise<IRuntimeRsponse> = (payload) => {
  // eslint-disable-next-line no-unused-expressions

  return api
    .service()
    .post('/api/Runtime/ExecuteProject', payload)
    .then((res) => res.data);
};
