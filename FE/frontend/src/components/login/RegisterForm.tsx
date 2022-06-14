import { yupResolver } from '@hookform/resolvers/yup';
import { ValidateErrorEntity } from 'rc-field-form/lib/interface';
import { FC } from 'react';
import { Controller, useForm } from 'react-hook-form';

import PButton from 'components/buttons/PButton';
import theme from 'theme/theme';
import { registrationSchema } from '../../util/validation-schema/ValidationSchema';
import { StyledButtonWrapper } from './LoginForm';
import {
  StyledForm,
  StyledFormBox,
  StyledFormInput,
  StyledFormSpan,
  StyledHeader,
} from './StyledForm';

interface ILoginFormProps {
  onSubmit: (values: unknown) => void;
  onFailed: ((errorInfo: ValidateErrorEntity<unknown>) => void) | undefined;
}

type FormValues = {
  name: string;
  lastname: string;
  email: string;
  password: string;
  confirmPassword: string;
  department: number;
  position: string;
};

const RegisterForm: FC<ILoginFormProps> = ({
  onSubmit,
  onFailed = undefined,
}) => {
  const {
    control,
    handleSubmit,
    formState: { errors },
  } = useForm<FormValues>({
    resolver: yupResolver(registrationSchema),
  });

  return (
    <>
      <StyledHeader>SignUp</StyledHeader>
      <StyledForm
        id="registerForm"
        onSubmit={handleSubmit(onSubmit)}
        autoComplete="off"
      >
        <Controller
          name="name"
          control={control}
          defaultValue=""
          render={({ field }) => (
            <StyledFormBox>
              <span>First name:</span>
              <StyledFormInput {...field} />
              <StyledFormSpan>{errors?.name?.message}</StyledFormSpan>
            </StyledFormBox>
          )}
        />

        <Controller
          name="lastname"
          control={control}
          defaultValue=""
          render={({ field }) => (
            <StyledFormBox>
              <span>Last name:</span>
              <StyledFormInput {...field} />
              <StyledFormSpan>{errors?.lastname?.message}</StyledFormSpan>
            </StyledFormBox>
          )}
        />

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

        <Controller
          name="confirmPassword"
          control={control}
          defaultValue=""
          render={({ field }) => (
            <StyledFormBox>
              <span>Confirm password:</span>
              <StyledFormInput.Password {...field} type="password" />
              <StyledFormSpan>
                {errors?.confirmPassword?.message}
              </StyledFormSpan>
            </StyledFormBox>
          )}
        />

        <Controller
          name="department"
          control={control}
          render={({ field }) => (
            <StyledFormBox>
              <span>Department:</span>
              <StyledFormInput {...field} />
              <StyledFormSpan>{errors?.department?.message}</StyledFormSpan>
            </StyledFormBox>
          )}
        />

        <Controller
          name="position"
          control={control}
          defaultValue=""
          render={({ field }) => (
            <StyledFormBox>
              <span>Position:</span>
              <StyledFormInput {...field} />
              <StyledFormSpan>{errors?.position?.message}</StyledFormSpan>
            </StyledFormBox>
          )}
        />
      </StyledForm>
      <StyledButtonWrapper>
        {/* <StyledLoginButton type="primary" htmlType="submit" form="registerForm">
          Register
        </StyledLoginButton> */}
        <PButton
          text="Register"
          color="#fff"
          borderRadius="8px"
          htmlType="submit"
          form="registerForm"
          background={theme.colors.primary}
        />
      </StyledButtonWrapper>
    </>
  );
};

export default RegisterForm;
