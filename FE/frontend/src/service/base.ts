import axios, {
  AxiosError,
  AxiosInstance,
  AxiosRequestConfig,
  AxiosResponse,
} from 'axios';
import { useNavigate } from 'react-router-dom';
import { StorageKey } from 'util/enums/storage-keys';
import { StatusCode } from '../util/enums/status-codes';

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
      const accessToken = localStorage.getItem(StorageKey.ACCESS_TOKEN);
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
  function (error: any) {
    // if (error.response.status === StatusCode.BADREQUEST) {
    //   window.location.href = '/verified';
    // }

    // if (
    //   !error.response ||
    //   error.response.status !== StatusCode.UNAUTHORIZED ||
    //   error.response.status !== StatusCode.BADREQUEST
    // ) {
    //   // bad request check wrong message bacnekd
    //   if (window.location.href !== 'http://localhost:3000/login') {
    //     window.location.href = '/login';
    //     localStorage.clear();
    //   }

    //   throw error;
    // }

    if (error.response.status === StatusCode.UNAUTHORIZED) {
      if (window.location.href === 'http://localhost:3000/login') {
        window.location.href = '/verification';
      } else {
        window.location.href = '/login';
        localStorage.clear();
      }
      // tu sam 2
      delete instance.defaults.headers.common.Authorization;
      // eslint-disable-next-line no-underscore-dangle
      const oldAccess = localStorage.getItem(StorageKey.ACCESS_TOKEN);
      localStorage.clear();
      window.location.href = '/login';
    }
    throw error;
  },
);

export default {
  service() {
    return instance;
  },
};
