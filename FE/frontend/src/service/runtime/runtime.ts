import api from '../base';
import { IRuntimeRequest } from './runtime.props';

export const getRuntime: () => Promise<string> = () => {
  return api.service().get('/api/runtime');
};

export const startRuntimeService: (
  payload: IRuntimeRequest,
) => Promise<string> = (payload) => {
  return api
    .service()
    .post('/api/runtime', payload)
    .then((res) => res.data);
};
