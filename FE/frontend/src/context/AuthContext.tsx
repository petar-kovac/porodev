import { message } from 'antd';
import { AxiosError, AxiosResponse } from 'axios';
import { createContext, FC, useContext, useState } from 'react';
import { loginApi, registerApi } from '../service/authorization/authorization';
import {
  ILoginResponse,
  IRegisterRequest,
  IRegisterResponse,
} from '../service/authorization/authorization.props';
import { StorageKey } from '../util/enums/enums';

type AuthContextProps = {
  isAuthenticated: boolean;
  setAuthenticated: (isAuthenticated: boolean) => void;
  login: (email: string, password: string) => Promise<void>;
  register: (
    name: string,
    lastname: string,
    email: string,
    password: string,
    department: number,
    position: string,
    avatarUrl: string,
  ) => Promise<void>;
  logout: () => void;
  testMessage: string;
  loggedUser: ILoginResponse | null;
};

export const AuthContext = createContext<AuthContextProps>({
  isAuthenticated: false,
  setAuthenticated(isAuthenticated: boolean): void {},
  async login(email: string, password: string): Promise<void> {
    // return new Promise((value: string | PromiseLike<string>) => void, );
  },
  async register(
    name: string,
    lastname: string,
    email: string,
    password: string,
    department: number,
    position: string,
    avatarUrl: string,
  ): Promise<void> {
    // return new Promise((value: string | PromiseLike<string>) => void, );
  },
  logout(): void {},
  testMessage: '',
  loggedUser: null,
});

export const AuthConsumer = AuthContext.Consumer;

const AuthProvider: FC<any> = ({ children }) => {
  const [isAuthenticated, setAuthenticated] = useState(false);
  const [loggedUser, setLoggedUser] = useState<ILoginResponse | null>(null);
  const testMessage = 'cedo-cedo ';

  const login: (email: string, password: string) => Promise<void> = async (
    email,
    password,
  ) => {
    try {
      const res: ILoginResponse = await loginApi({
        email,
        password,
      });
      message.success('Successful login');
      localStorage.setItem(StorageKey.NAME.valueOf(), res.name);
      localStorage.setItem(StorageKey.LASTNAME.valueOf(), res.lastname);
      setLoggedUser(res);
      setAuthenticated(true);
    } catch (err: any) {
      message.error(err.message);
    }
  };
  const register: (
    name: string,
    lastname: string,
    email: string,
    password: string,
    department: number,
    position: string,
    avatarUrl: string,
  ) => Promise<void> = async (
    name,
    lastname,
    email,
    password,
    department,
    position,
    avatarUrl,
  ) => {
    try {
      const res: IRegisterResponse = await registerApi({
        name,
        lastname,
        email,
        password,
        department,
        position,
        avatarUrl,
      });
      localStorage.setItem(StorageKey.NAME.valueOf(), res.name);
      localStorage.setItem(StorageKey.LASTNAME.valueOf(), res.lastname);
      message.success('Successful registration');
      setAuthenticated(true);
    } catch (err: any) {
      message.error(err.message);
    }
  };

  const logout: () => void = () => {
    message.success('Logged out');
    setAuthenticated(false);
  };

  const state: AuthContextProps = {
    isAuthenticated,
    setAuthenticated,
    logout,
    login,
    register,
    loggedUser,
    testMessage,
  };
  return <AuthContext.Provider value={state}>{children}</AuthContext.Provider>;
};

export const useAuthStateValue: () => AuthContextProps = () =>
  useContext(AuthContext);

export default AuthProvider;
