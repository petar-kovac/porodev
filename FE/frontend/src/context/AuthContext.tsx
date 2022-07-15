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

import { verifyEmail } from 'service/files/files';

import {
  useLocation,
  useNavigate,
  useParams,
  useSearchParams,
} from 'react-router-dom';
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
  const navigate = useNavigate();

  const { search } = useLocation();
  const searchParams = new URLSearchParams(search);
  const searchEmail = searchParams.get('Email');
  const searchToken = searchParams.get('Token');

  useEffect(() => {
    if (searchEmail !== null && searchToken !== null) {
      verifyEmail(searchEmail, searchToken)
        .then((response: any) => {
          console.log(response);
          if (response.status === 200) {
            navigate('/confirm');
          }

          setAuthenticated(false);
          setIsLoading(false);
        })
        .catch((error: any) => {
          // console.log(error);

          if (error.response.status === 400) {
            navigate('/verified');
          }
          message.error(error.message);
        })
        .finally(() => {
          setIsLoading(false);
        });
    } else {
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

        if (res.position === '1') {
          setIsAdmin(true);
          navigate('/');
        } else {
          navigate('/user-home');
        }
        console.log(res);
        setAuthenticated(true);
        setIsLoading(false);
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

        message.success('Successful registration');

        navigate('/verify');
        setIsLoading(false);
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
