import * as yup from 'yup';

const nameRegex = /^[a-zA-Z]+$/g;
const emailRegex = /^[a-zA-Z0-9.\-_]+@boing\.rs$/g;
const passwordRegex =
  /^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^\w\s]).{8,}$/g;
const positionRegex = /^[a-zA-Z\s]*$/g;

export const registrationSchema = yup.object().shape({
  name: yup
    .string()
    .transform((value, originalValue) => {
      return originalValue === '' ? undefined : value;
    })
    .matches(nameRegex, 'Only letters allowed')
    .min(1, 'Must be > 1 character')
    .max(20, 'Must be < 20 characters')
    .required('This field is required'),
  lastname: yup
    .string()
    .transform((value, originalValue) => {
      return originalValue === '' ? undefined : value;
    })
    .matches(nameRegex, 'Only letters allowed')
    .min(1, 'Must be > 1 character')
    .max(20, 'Must be < 20 characters')
    .required('This field is required'),
  email: yup
    .string()
    .transform((value, originalValue) => {
      return originalValue === '' ? undefined : value;
    })
    .min(1, 'Must be > 1 character')
    .max(50, 'Must be < 50 characters')
    .required('This field is required')
    .matches(emailRegex, 'Email is invalid'),
  password: yup
    .string()
    .required('This field is required')
    .matches(/^\S*$/, 'Whitespace not allowed')
    .matches(passwordRegex, 'Wrong password'),
  confirmPassword: yup
    .string()
    .required('This field is required')
    .oneOf([yup.ref('password'), null], 'Passwords should match'),
  // department: yup
  //   .number()
  //   .positive()
  //   .transform((value, originalValue) => {
  //     return originalValue === '' ? undefined : value;
  //   })
  //   .min(0, 'Must be > 0')
  //   .nullable(true)
  //   .typeError('Must be a number')
  //   .required('This field is required'),
  position: yup
    .string()
    .transform((value, originalValue) => {
      return originalValue === '' ? undefined : value;
    })
    .matches(positionRegex, 'Letters & whitespace only')
    .min(1, 'Must be > 1 character')
    .max(50, 'Must be < 50 characters')
    .required('This field is required'),
});

export const loginSchema = yup.object().shape({
  email: yup
    .string()
    .transform((value, originalValue) => {
      return originalValue === '' ? undefined : value;
    })
    .min(1, 'Must be > 1 character')
    .max(50, 'Must be < 50 characters')
    .required('This field is required')
    .matches(emailRegex, 'Email is invalid'),
  password: yup
    .string()
    .required('This field is required')
    .matches(/^\S*$/, 'Whitespace not allowed')
    .matches(passwordRegex, 'Wrong password'),
});

export const passwordSchema = yup.object().shape({
  oldPassword: yup
    .string()
    .required('This field is required')
    .matches(/^\S*$/, 'Whitespace not allowed')
    .matches(passwordRegex, 'Wrong password'),
  newPassword: yup
    .string()
    .required('This field is required')
    .matches(/^\S*$/, 'Whitespace not allowed')
    .matches(passwordRegex, 'Wrong password'),
  confirmPassword: yup
    .string()
    .required('This field is required')
    .oneOf([yup.ref('newPassword'), null], 'Passwords should match'),
});
