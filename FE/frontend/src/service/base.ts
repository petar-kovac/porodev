import axios, { AxiosRequestHeaders } from 'axios';
import { StatusCode } from '../util/enums/enums';
import config from '../util/config';

interface Config {
  baseURL: string | undefined;
}

const api = (dontUseAuthorizationHeader?: any) => {
  const baseConfig: Config = {
    baseURL: config.BASE_ENDPOINT_URL,
  };
  const createInstance = (baseConfig: any) => {
    const instance = axios.create(baseConfig);
    instance.defaults.headers.common['Content-Type'] = 'application/json';
    instance.defaults.headers.common['Access-Control-Allow-Origin'] = '*';
    instance.interceptors.request.use(
      async (config) => {
        if (dontUseAuthorizationHeader) return config;

        const token = await sessionStorage.getItem('accessToken');
        if (token && config.url !== '/login') {
          config.headers = {
            ...config.params,
            Authorization: `Bearer ${token}`,
          };
          // config.headers['Content-Type'] = 'application/json';
        }
        return config;
      },
      (error) => {
        Promise.reject(error);
      },
    );
    return instance;
  };
  const instance = createInstance(baseConfig);
  return instance;
};
export default api;
