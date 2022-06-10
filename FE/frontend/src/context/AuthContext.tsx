import { message } from 'antd';
import {
  createContext,
  FC,
  ReactNode,
  useCallback,
  useContext,
  useEffect,
  useMemo,
  useState,
} from 'react';
import { useLocation, useNavigate } from 'react-router-dom';

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
  isAdmin: boolean;
  isLoading: boolean;
};

export const AuthContext = createContext<AuthContextProps>({
  isAuthenticated: false,
  setAuthenticated: (isAuthenticated: boolean) => {},
  login: async (loginData: ILoginRequest) => {},
  register: async (registerData: IRegisterRequest) => {},
  logout: () => {},
  testMessage: '',
  loggedUser: null,
  isAdmin: false,
  isLoading: true,
});

export const AuthConsumer = AuthContext.Consumer;

const AuthProvider: FC<{ children: ReactNode }> = ({ children }) => {
  const [isAuthenticated, setAuthenticated] = useState(false);
  const [loggedUser, setLoggedUser] = useState<ILoginResponse | null>(null);
  const [isAdmin, setIsAdmin] = useState(false);
  const [isLoading, setIsLoading] = useState<boolean>(true);
  const testMessage = 'test-test ';
  const location = useLocation();
  const navigate = useNavigate();

  useEffect(() => {
    const getLastname = localStorage.getItem(StorageKey.LASTNAME);
    // because backend is not finished, checking if its admin, if user's lastname is Admin
    if (getLastname) {
      if (getLastname === 'Admin') {
        setIsAdmin(true);
      }
      setAuthenticated(true);
      setIsLoading(false);

      // change this when backend is implemented
      if (isAdmin && location.pathname === '/') {
        navigate('/');
      } else {
        navigate('/user-home');
      }

      navigate(location);
    } else {
      navigate('/login');
    }
    setIsLoading(false);
  }, []);

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
        localStorage.setItem(StorageKey.ACCESS_TOKEN, res.jwt);

        // mocked this until backend has been implemented
        if (res.lastname === 'Admin') {
          setIsAdmin(true);
        }
        setAuthenticated(true);
        setIsLoading(false);

        // mocked this until backend has been implemented
        if (res.lastname === 'Admin') {
          navigate('/');
        } else {
          navigate('/user-home');
        }
      } catch (err: any) {
        message.error(err.message);
        navigate('/login');
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

        // mocked this until backend has been implemented
        if (res.lastname === 'Admin') {
          setIsAdmin(true);
        }
        setAuthenticated(true);
        setIsLoading(false);

        if (res.lastname === 'Admin') {
          navigate('/');
        } else {
          navigate('/user-home');
        }
      } catch (err: any) {
        message.error(err.message);
        navigate('/login');
      }
    }, []);

  const logout: () => void = useCallback(() => {
    localStorage.removeItem(StorageKey.NAME);
    localStorage.removeItem(StorageKey.LASTNAME);
    message.success('Logged out');
    setIsAdmin(false);
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
      isAdmin,
      isLoading,
    }),
    [
      isAuthenticated,
      setAuthenticated,
      logout,
      login,
      register,
      loggedUser,
      testMessage,
      isAdmin,
      isLoading,
    ],
  );
  return <AuthContext.Provider value={state}>{children}</AuthContext.Provider>;
};

export const useAuthStateValue: () => AuthContextProps = () =>
  useContext(AuthContext);

export default AuthProvider;
