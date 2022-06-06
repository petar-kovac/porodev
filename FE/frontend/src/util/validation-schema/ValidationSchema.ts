import { message } from 'antd';
import * as yup from 'yup';

const emailRegex = /^[a-zA-Z0-9.-_]+@boing\.rs$/g;
const passwordRegex =
  /^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^\w\s]).{8,}$/g;

export const registrationSchema = yup.object().shape({
  firstName: yup.string().required(),
  lastName: yup.string().required(),
  email: yup.string().email().matches(emailRegex, 'Invalid email').required(),
  password: yup.string().matches(passwordRegex, 'Wrong password').required(),
  confirmPassword: yup.string().oneOf([yup.ref('password'), null]),
  department: yup.number().positive().integer().required(),
  position: yup.string().required(),
});

export const loginSchema = yup.object().shape({
  email: yup.string().email().matches(emailRegex, 'Invalid email').required(),
  password: yup.string().matches(passwordRegex, 'Wrong password').required(),
});
