import { FC, MouseEventHandler, useState } from 'react';
import styled from 'styled-components';

import logoImage from 'assets/logo.png';
import LoginForm from 'components/login/LoginForm';
import RegisterForm from 'components/login/RegisterForm';
import { StyledPage, StyledFormWrapper } from 'components/login/StyledForm';
import { StyledToggleButton } from 'components/buttons/buttons';

import {
  ILoginRequest,
  IRegisterRequest,
} from 'service/authorization/authorization.props';

import { useAuthStateValue } from 'context/AuthContext';
import PButton from 'components/buttons/PButton';
import theme from 'theme/theme';

const Login: FC = () => {
  const [isLogin, setIsLogin] = useState<boolean>(true);
  const { login, register } = useAuthStateValue();
  const [isLoading, setIsLoading] = useState<boolean>(false);

  const handleLogin = (values: unknown) => {
    login(values as ILoginRequest);
  };
  const handleRegister = (values: unknown) => {
    register(values as IRegisterRequest);
  };
  const handleFormChange: MouseEventHandler<HTMLButtonElement> = (e) => {
    e.preventDefault();
    e.stopPropagation();
    setIsLogin((value) => !value);
  };

  return (
    <StyledPage>
      <StyledFormWrapper>
        {isLogin ? (
          <LoginForm
            isLoading={isLoading}
            onSubmit={handleLogin}
            onFailed={undefined}
          />
        ) : (
          <RegisterForm onSubmit={handleRegister} onFailed={undefined} />
        )}

        <PButton
          text={isLogin ? 'Create new account' : 'Login with existing account'}
          color="#777"
          borderRadius="8px"
          htmlType="submit"
          form="registerForm"
          background="#fcfcfc"
          onClick={handleFormChange}
        />
      </StyledFormWrapper>
      <StyledLogo src={logoImage} />
    </StyledPage>
  );
};

export default Login;

const StyledLogo = styled.img`
  position: absolute;
  top: 5rem;
  right: 10rem;
  width: 25rem;
`;
