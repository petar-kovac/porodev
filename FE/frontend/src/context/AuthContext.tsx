import { message } from 'antd';
import JwtDecode from 'jwt-decode';
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
import { verifyEmail } from 'service/files/files';
import { Role } from 'util/enums/roles';

import { useLocation, useNavigate } from 'react-router-dom';
import { StorageKey } from 'util/enums/storage-keys';

import { loginApi, registerApi } from '../service/authorization/authorization';
import {
  ILoginRequest,
  ILoginResponse,
  IRegisterRequest,
  IRegisterResponse,
} from '../service/authorization/authorization.props';

type AuthContextProps = {
  isAuthenticated: boolean;
  setAuthenticated: (isAuthenticated: boolean) => void;
  login: (loginData: ILoginRequest) => Promise<void>;
  register: (registerData: IRegisterRequest) => Promise<void>;
  logout: () => void;
  loggedUser: string;
  isAdmin: boolean;
  isLoading: boolean;
};
interface IToken {
  exp: number;
}

export const AuthContext = createContext<AuthContextProps>({
  isAuthenticated: false,
  setAuthenticated: (isAuthenticated: boolean) => {},
  login: async (loginData: ILoginRequest) => {},
  register: async (registerData: IRegisterRequest) => {},
  logout: () => {},
  loggedUser: '',
  isAdmin: false,
  isLoading: true,
});

export const AuthConsumer = AuthContext.Consumer;

const AuthProvider: FC<{ children: ReactNode }> = ({ children }) => {
  const [isAuthenticated, setAuthenticated] = useState(false);
  const [loggedUser, setLoggedUser] = useState<string>('');
  const [isAdmin, setIsAdmin] = useState(false);
  const [isLoading, setIsLoading] = useState<boolean>(true);

  const navigate = useNavigate();
  const { search, pathname } = useLocation();

  const searchParams = new URLSearchParams(search);
  const searchEmail = searchParams.get('Email');
  const searchToken = searchParams.get('Token');

  useEffect(() => {
    // first check if is it tryting to verify on initialization
    if (searchEmail !== null && searchToken !== null) {
      verifyEmail(searchEmail, searchToken)
        .then((response: any) => {
          if (response.status === 200) {
            navigate('/confirm');
          }
          setIsLoading(false);
        })
        .catch((error: any) => {
          if (error.response.status === 400) {
            navigate('/verified');
          }

          // if (error.response.status === 401) {
          //   navigate('/verify');
          // }

          message.error(error.message);
        })
        .finally(() => {
          setIsLoading(false);
        });
    } else {
      // try to go directrly on a route or F5 behaviour
      const currentDate = Math.round(new Date().getTime() / 1000);
      const accessToken = localStorage.getItem(StorageKey.ACCESS_TOKEN);
      const localRole = localStorage.getItem(StorageKey.ROLE);
      try {
        if (accessToken) {
          const tokenDecoded: IToken = JwtDecode(accessToken);
          if (currentDate < tokenDecoded.exp) {
            // need to save role in storage because it is not implemented in the token
            if (localRole === Role.ADMIN) {
              setLoggedUser(Role.ADMIN);
              setAuthenticated(true);
              navigate(pathname);
            }
            if (localRole === Role.USER) {
              setLoggedUser(Role.USER);
              setAuthenticated(true);
              navigate(pathname);
            }
          } else {
            setAuthenticated(false);
            setIsLoading(false);
            localStorage.clear();
            navigate('/login');
            throw new Error('Invalid token');
          }
        } else if (pathname === '/verify') {
          navigate('/verify');
        } else {
          setAuthenticated(false);
          setIsLoading(false);
          navigate('/login');
          localStorage.clear();
        }
      } catch (error: any) {
        message.error(error.message);
      }
      setIsLoading(false);
    }
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
        localStorage.setItem(StorageKey.EMAIL, res.email);
        localStorage.setItem(StorageKey.ACCESS_TOKEN, res.jwt);
        localStorage.setItem(StorageKey.ROLE, res.role);

        if (res.role === Role.ADMIN) {
          setLoggedUser(Role.ADMIN);
          navigate('/');
        } else if (res.role === Role.USER) {
          setLoggedUser(Role.USER);
          navigate('/user-home');
        } else {
          throw new Error('No user role');
        }
        setAuthenticated(true);
        setIsLoading(false);
      } catch (error: any) {
        message.error(error.message);
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

        message.success('Successful registration');

        navigate('/verify');
        setIsLoading(false);
      } catch (err: any) {
        message.error(err.message);
        navigate('/login');
      }
    }, []);

  const logout: () => void = useCallback(() => {
    message.success('Logged out');
    localStorage.clear();
    setIsAdmin(false);
    setAuthenticated(false);
    navigate('/login');
  }, []);

  const state: AuthContextProps = useMemo(
    () => ({
      isAuthenticated,
      setAuthenticated,
      logout,
      login,
      register,
      loggedUser,
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
      isAdmin,
      isLoading,
    ],
  );
  return <AuthContext.Provider value={state}>{children}</AuthContext.Provider>;
};

export const useAuthStateValue: () => AuthContextProps = () =>
  useContext(AuthContext);

export default AuthProvider;
