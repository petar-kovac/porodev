import { FC } from 'react';
import { ValidateErrorEntity } from 'rc-field-form/lib/interface';
import { useForm, Controller } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import styled from 'styled-components';

import theme from 'theme/theme';

import { useAuthStateValue } from 'context/AuthContext';

import { loginSchema } from 'util/validation-schema/ValidationSchema';
import PButton from 'components/buttons/PButton';
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
  // isLoading: boolean;
}

type IFormValues = {
  email: string;
  password: string;
};

const LoginForm: FC<ILoginFormProps> = ({
  // isLoading,
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

  const { isLoading } = useAuthStateValue();

  return (
    <>
      <StyledHeader>Login</StyledHeader>
      <StyledForm id="loginForm" autoComplete="off">
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
        <PButton
          text="Login"
          color="#fff"
          radius="8px"
          htmlType="submit"
          form="loginForm"
          background={theme.colors.primary}
          onClick={handleSubmit(onSubmit)}
          isLoading={isLoading}
        />
        <StyledSpace>
          {/*  <StyledSpinWrapper>
            {isLoading && <Spinner color="#000" size={24} speed={1.2} />} 
          </StyledSpinWrapper> */}
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
  margin-bottom: 8px;
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
