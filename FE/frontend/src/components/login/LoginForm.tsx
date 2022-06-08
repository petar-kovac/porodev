import { FC } from 'react';
import { ValidateErrorEntity } from 'rc-field-form/lib/interface';
import '../../App.css';
import { useForm, Controller } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import { loginSchema } from '../../util/validation-schema/ValidationSchema';
import {
  StyledHeader,
  StyledForm,
  StyledFormBox,
  StyledFormInput,
  StyledFormSpan,
  StyledLoginButton,
} from './StyledForm';

interface ILoginFormProps {
  onSubmit: (values: unknown) => void;
  onFailed: ((errorInfo: ValidateErrorEntity<unknown>) => void) | undefined;
}

type IFormValues = {
  email: string;
  password: string;
};

const LoginForm: FC<ILoginFormProps> = ({ onSubmit, onFailed = undefined }) => {
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
      <StyledLoginButton type="primary" htmlType="submit" form="loginForm">
        Login
      </StyledLoginButton>
    </>
  );
};

export default LoginForm;
