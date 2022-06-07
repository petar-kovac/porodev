import { FC, useState } from 'react';
import LoginForm from '../components/login/LoginForm';
import RegisterForm from '../components/login/RegisterForm';
import {
  StyledPage,
  StyledFormWrapper,
  StyledToggleButton,
} from '../components/login/StyledForm';
import { useAuthStateValue } from '../context/AuthContext';
import {
  ILoginRequest,
  IRegisterRequest,
} from '../service/authorization/authorization.props';

const Login: FC = () => {
  const [isLogin, setIsLogin] = useState<boolean>(true);
  const { login, register } = useAuthStateValue();

  const handleLogin = (values: unknown) => {
    login(values as ILoginRequest);
  };
  const handleRegister = (values: unknown) => {
    register(values as IRegisterRequest);
  };
  const handleFormChange = () => setIsLogin((value) => !value);

  return (
    <StyledPage>
      <StyledFormWrapper>
        {isLogin ? (
          <LoginForm onSubmit={handleLogin} onFailed={undefined} />
        ) : (
          <RegisterForm onSubmit={handleRegister} onFailed={undefined} />
        )}
        <StyledToggleButton type="default" onClick={handleFormChange}>
          {isLogin ? 'Create new account' : 'Login with existing account'}
        </StyledToggleButton>
      </StyledFormWrapper>
    </StyledPage>
  );
};

export default Login;
