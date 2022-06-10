import axios, {
  AxiosError,
  AxiosInstance,
  AxiosRequestConfig,
  AxiosResponse,
} from 'axios';

import { StatusCode, StorageKey } from '../util/enums/enums';

interface Config {
  baseURL: string | undefined;
}

const baseUrl = process.env.REACT_APP_BASE_ENDPOINT_URL;
const baseConfig: Config = {
  baseURL: baseUrl,
};

const createInstance = (mainConfig: Config): AxiosInstance => {
  const instance = axios.create(mainConfig);
  instance.defaults.headers.common['Access-Control-Allow-Origin'] = '*';
  instance.defaults.headers.common['Content-Type'] = 'application/json';
  instance.interceptors.request.use(
    async (config: AxiosRequestConfig) => {
      const accessToken = await localStorage.getItem(StorageKey.ACCESS_TOKEN);
      const newConfig = { ...config };
      if (!!accessToken && newConfig?.headers) {
        newConfig.headers.Authorization = `Bearer ${accessToken}`;
      }
      return config;
    },
    (error: AxiosError) => {
      Promise.reject(error);
    },
  );
  return instance;
};

const instance = createInstance(baseConfig);

instance.interceptors.response.use(
  (response: any) => {
    return response;
  },
  // eslint-disable-next-line consistent-return
  async function (error: any) {
    const originalRequest = error.config;
    if (!error.response || error.response.status !== StatusCode.UNAUTHORIZED) {
      throw error;
    }

    if (
      error.response.status === StatusCode.UNAUTHORIZED &&
      originalRequest.url !== '/refresh' &&
      // eslint-disable-next-line no-underscore-dangle
      !originalRequest._retry
    ) {
      delete instance.defaults.headers.common.Authorization; // mentor suggestion to check
      // eslint-disable-next-line no-underscore-dangle
      originalRequest._retry = true;
      const oldAccess = await localStorage.getItem(StorageKey.ACCESS_TOKEN);
      const oldRefresh = await localStorage.getItem(StorageKey.REFRESH_TOKEN);
      if (oldAccess) {
        try {
          const { accessToken, refreshToken } = await instance
            .post(
              '/refresh',
              { accessToken: oldAccess, refreshToken: oldRefresh },
              {
                transformRequest: [
                  (data: any, headers: any) => {
                    if (headers) {
                      // eslint-disable-next-line no-param-reassign
                      delete headers.Authorization;
                      // eslint-disable-next-line no-param-reassign
                      headers['Content-Type'] = 'application/json';
                    }
                    return JSON.stringify({
                      accessToken: oldAccess,
                      refreshToken: oldRefresh,
                    });
                  },
                ],
              },
            )
            .then((response: AxiosResponse) => response.data);

          await localStorage.setItem(StorageKey.ACCESS_TOKEN, accessToken);
          await localStorage.setItem(StorageKey.REFRESH_TOKEN, refreshToken);
          instance.defaults.headers.common.Authorization = `Bearer ${accessToken}`;
          return await instance(originalRequest);
        } catch (err) {
          if (originalRequest.url !== '/login') {
            await localStorage.clear();
          }

          throw err;
        }
      }
    } else {
      await localStorage.clear();
      window.location.href = '/login';
      throw error;
    }
  },
);

export default {
  service() {
    return instance;
  },
};
