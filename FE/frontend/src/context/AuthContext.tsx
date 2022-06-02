import { message } from 'antd';
import { AxiosError, AxiosResponse } from 'axios';
import {
  createContext,
  FC,
  ReactNode,
  useCallback,
  useContext,
  useMemo,
  useState,
} from 'react';
import { loginApi, registerApi } from '../service/authorization/authorization';
import {
  ILoginRequest,
  ILoginResponse,
  IRegisterRequest,
  IRegisterResponse,
} from '../service/authorization/authorization.props';
import { StorageKey } from '../util/enums/enums';

type AuthContextProps = {
  isAuthenticated: boolean;
  setAuthenticated: (isAuthenticated: boolean) => void;
  login: (loginData: ILoginRequest) => Promise<void>;
  register: (registerData: IRegisterRequest) => Promise<void>;
  logout: () => void;
  testMessage: string;
  loggedUser: ILoginResponse | null;
};

export const AuthContext = createContext<AuthContextProps>({
  isAuthenticated: false,
  setAuthenticated(isAuthenticated: boolean): void {},
  async login(loginData: ILoginRequest): Promise<void> {
    // return new Promise((value: string | PromiseLike<string>) => void, );
  },
  async register(registerData: IRegisterRequest): Promise<void> {
    // return new Promise((value: string | PromiseLike<string>) => void, );
  },
  logout(): void {},
  testMessage: '',
  loggedUser: null,
});

export const AuthConsumer = AuthContext.Consumer;

const AuthProvider: FC<{ children: ReactNode }> = ({ children }) => {
  const [isAuthenticated, setAuthenticated] = useState(false);
  const [loggedUser, setLoggedUser] = useState<ILoginResponse | null>(null);
  const testMessage = 'cedo-cedo ';

  const login: (loginData: ILoginRequest) => Promise<void> = useCallback(
    async (loginData: ILoginRequest) => {
      try {
        const res: ILoginResponse = await loginApi({
          email: loginData.email,
          password: loginData.password,
        });
        message.success('Successful login');
        localStorage.setItem(StorageKey.NAME, res.name);
        localStorage.setItem(StorageKey.LASTNAME, res.lastname);
        setLoggedUser(res);
        setAuthenticated(true);
      } catch (err: any) {
        message.error(err.message);
      }
    },
    [],
  );
  const register: (registerData: IRegisterRequest) => Promise<void> =
    useCallback(async (registerData: IRegisterRequest) => {
      try {
        const res: IRegisterResponse = await registerApi({
          name: registerData.name,
          lastname: registerData.lastname,
          email: registerData.email,
          password: registerData.password,
          department: registerData.department,
          position: registerData.position,
          avatarUrl: 'aurl',
        });
        localStorage.setItem(StorageKey.NAME, res.name);
        localStorage.setItem(StorageKey.LASTNAME, res.lastname);
        message.success('Successful registration');
        setAuthenticated(true);
      } catch (err: any) {
        message.error(err.message);
      }
    }, []);

  const logout: () => void = useCallback(() => {
    message.success('Logged out');
    setAuthenticated(false);
  }, []);

  const state: AuthContextProps = useMemo(
    () => ({
      isAuthenticated,
      setAuthenticated,
      logout,
      login,
      register,
      loggedUser,
      testMessage,
    }),
    [
      isAuthenticated,
      setAuthenticated,
      logout,
      login,
      register,
      loggedUser,
      testMessage,
    ],
  );
  return <AuthContext.Provider value={state}>{children}</AuthContext.Provider>;
};

export const useAuthStateValue: () => AuthContextProps = () =>
  useContext(AuthContext);

export default AuthProvider;
