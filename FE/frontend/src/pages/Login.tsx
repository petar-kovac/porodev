import { useState } from 'react';
import { Button } from 'antd';
import styled from 'styled-components';
import LoginForm from '../components/login/LoginForm';
import RegisterForm from '../components/login/RegisterForm';
import {
  StyledPage,
  StyledFormWrapper,
} from '../components/styled/StyledLoginPage';
import { useAuthStateValue } from '../context/AuthContext';
import {
  ILoginRequest,
  IRegisterRequest,
} from '../service/authorization/authorization.props';

const Login: React.FC = () => {
  const [isLogin, setIsLogin] = useState<boolean>(true);
  const { login, register } = useAuthStateValue();

  const handleLogin = (values: ILoginRequest) =>
    login(values.email, values.password);
  const handleRegister = (values: IRegisterRequest) =>
    register(
      values.name,
      values.lastname,
      values.email,
      values.password,
      values.department,
      values.position,
      'avatarurl',
    );
  const handleFormChange = () => setIsLogin((value) => !value);

  return (
    <StyledPage>
      <StyledFormWrapper>
        {isLogin ? (
          <LoginForm onSubmit={handleLogin} onFailed={undefined} />
        ) : (
          <RegisterForm onSubmit={handleRegister} onFailed={undefined} />
        )}
        <StyledLoginButton type="default" onClick={handleFormChange}>
          {isLogin ? 'Create new account' : 'Login with existing account'}
        </StyledLoginButton>
      </StyledFormWrapper>
    </StyledPage>
  );
};

export default Login;

const StyledLoginButton = styled(Button)`
  border-radius: 8px;
  margin-top: 1rem;
  color: #777;
  background: #fcfcfc;
  border: none;
  outline: none;

  &:hover,
  &:focus,
  &:active {
    color: #777;
    box-shadow: 1px 2px 6px 1px rgba(0, 0, 0, 0.1);
  }
`;
