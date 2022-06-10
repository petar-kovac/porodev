import { FC } from 'react';
import { ValidateErrorEntity } from 'rc-field-form/lib/interface';
import { useForm, Controller } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import styled from 'styled-components';

import { loginSchema } from 'util/validation-schema/ValidationSchema';
import { StyledLoginButton } from 'components/buttons/buttons';
import {
  StyledHeader,
  StyledForm,
  StyledFormBox,
  StyledFormInput,
  StyledFormSpan,
} from './StyledForm';
import Spinner from '../spinner/Spinner';

interface ILoginFormProps {
  onSubmit: (values: unknown) => void;
  onFailed: ((errorInfo: ValidateErrorEntity<unknown>) => void) | undefined;
  isLoading: boolean;
}

type IFormValues = {
  email: string;
  password: string;
};

const LoginForm: FC<ILoginFormProps> = ({
  isLoading,
  onSubmit,
  onFailed = undefined,
}) => {
  const {
    control,
    handleSubmit,
    formState: { errors },
  } = useForm<IFormValues>({
    resolver: yupResolver(loginSchema),
  });

  return (
    <>
      <StyledHeader>Login</StyledHeader>
      <StyledForm
        id="loginForm"
        onSubmit={handleSubmit(onSubmit)}
        autoComplete="off"
      >
        <Controller
          name="email"
          control={control}
          defaultValue=""
          render={({ field }) => (
            <StyledFormBox>
              <span>E-mail:</span>
              <StyledFormInput {...field} />
              <StyledFormSpan>{errors?.email?.message}</StyledFormSpan>
            </StyledFormBox>
          )}
        />

        <Controller
          name="password"
          control={control}
          defaultValue=""
          render={({ field }) => (
            <StyledFormBox>
              <span>Password:</span>
              <StyledFormInput.Password
                {...field}
                type="password"
                autoComplete="new-password"
              />
              <StyledFormSpan>{errors?.password?.message}</StyledFormSpan>
            </StyledFormBox>
          )}
        />
      </StyledForm>
      <StyledButtonWrapper>
        <StyledSpace />
        <StyledLoginButton type="primary" htmlType="submit" form="loginForm">
          Login
        </StyledLoginButton>
        <StyledSpace>
          <StyledSpinWrapper>
            {isLoading && <Spinner color="#000" size={24} speed={1.2} />}
          </StyledSpinWrapper>
        </StyledSpace>
      </StyledButtonWrapper>
    </>
  );
};
export const StyledButtonWrapper = styled.div`
  display: flex;
  width: 100%;
  align-items: flex-end;
  height: 62px;
  justify-content: space-around;
`;
const StyledSpinWrapper = styled.div`
  padding-bottom: 4px;
  margin-left: 8px;
`;
const StyledSpace = styled.div`
  display: flex;
  flex: 1;
`;

export default LoginForm;
