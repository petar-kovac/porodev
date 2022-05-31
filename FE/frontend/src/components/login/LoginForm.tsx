import { FC } from 'react';
import { Form, Input, Button } from 'antd';
import { ValidateErrorEntity } from 'rc-field-form/lib/interface';
import styled from 'styled-components';
import '../../App.css';

interface ILoginFormProps {
  onSubmit: (values: any) => void;
  onFailed: ((errorInfo: ValidateErrorEntity<unknown>) => void) | undefined;
}

const LoginForm: FC<ILoginFormProps> = ({ onSubmit, onFailed = undefined }) => {
  // const [isLogin, setIsLogin] = useState<boolean>(true);

  // console.log(isLogin, 'lg');
  // const onFinish = (values: any) => {
  //   console.log(values);

  //   if (isLogin) {
  //     try {
  //       const res = async () => {
  //         axios({
  //           method: 'post',

  //           url: 'https://localhost:7151/api/user/login',
  //           data: {
  //             ...values,
  //           },
  //           headers: {
  //             'Content-Type': 'application/json; charset=utf-8',
  //             'Access-Control-Allow-Origin': '*',
  //           },
  //         });
  //       };
  //       res();
  //     } catch (err) {
  //       console.log('ok');
  //     }
  //   } else {
  //     const res = async () => {
  //       axios({
  //         method: 'post',
  //         url: 'https://localhost:7151/api/user/register/User',
  //         data: {
  //           ...values,
  //           avatarUrl: 'url',
  //         },
  //         headers: {
  //           'Content-Type': 'application/json; charset=utf-8',
  //           'Access-Control-Allow-Origin': '*',
  //         },
  //       });
  //     };
  //     res();
  //   }
  // };

  // const onFinishFailed = (errorInfo: any) => {
  //   console.log('Failed:', errorInfo);
  // };

  return (
    <>
      <StyledHeader>Login</StyledHeader>
      <StyledForm
        name="basic"
        labelCol={{ span: 8 }}
        wrapperCol={{ span: 16 }}
        initialValues={{ remember: true }}
        onFinish={onSubmit}
        onFinishFailed={onFailed}
        autoComplete="off"
      >
        <Form.Item
          label="Email"
          name="email"
          rules={[{ required: true, message: 'Please input your username' }]}
        >
          <Input />
        </Form.Item>

        <Form.Item
          label="Password"
          name="password"
          rules={[{ required: true, message: 'Please input your password' }]}
        >
          <Input.Password />
        </Form.Item>
      </StyledForm>
      <StyledLoginButton type="primary" htmlType="submit">
        Login
      </StyledLoginButton>
    </>
  );
};

export default LoginForm;

const StyledLoginButton = styled(Button)`
  border-radius: 8px;

  &:hover,
  &:focus {
    box-shadow: 1px 2px 4px 1px rgba(0, 0, 0, 0.1);
    background-color: #47a6ff;
  }
`;

const StyledHeader = styled.h1`
  font-size: 32px;
  color: #ffffff;
  opacity: 0.9;
`;

const StyledForm = styled(Form)`
  & input,
  & span {
    opacity: 0.75;
    border-radius: 10px;
  }

  & input:active,
  & input:focus {
    opacity: 1;
  }

  & label {
    color: rgb(0, 0, 0, 0.5);
    font-weight: bold;
  }
`;
