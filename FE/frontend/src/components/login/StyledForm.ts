import { Input, Select } from 'antd';

import styled from 'styled-components';

import landingImage from '../../assets/landing.jpg';

export const StyledPage = styled.div`
  display: flex;
  flex-direction: column;
  justify-content: center;
  height: 100vh;
  background-image: url(${landingImage});
  background-size: cover;
  background-position: center;
  background-repeat: no-repeat;
`;

export const StyledFormWrapper = styled.div`
  display: flex;
  flex-direction: column;
  padding: 3rem 0;
  max-width: 40rem;
  margin: 0 16rem;
  border-radius: 12px;
  box-shadow: -1px 1px 10px -1px #ffffff;
  justify-content: center;
  align-items: center;
  background-image: linear-gradient(
    to left bottom,
    rgba(92, 159, 231, 0.1),
    rgba(221, 233, 246, 0.9)
  );
`;

export const StyledHeader = styled.h1`
  font-size: 32px;
  color: #ffffff;
  opacity: 0.9;
`;

export const StyledForm = styled.form`
  display: flex;
  flex-direction: column;
  gap: 1.2rem;
  width: 32rem;

  & input,
  & select,
  & span {
    opacity: 0.75;
    border-radius: 10px;
  }

  & input:active,
  & select,
  & input:focus {
    opacity: 1;
  }
`;

export const StyledFormInput = styled(Input)`
  background-color: #fff;
`;

export const StyledFormBox = styled.div`
  display: flex;
  flex-direction: column;
  position: relative;
`;

export const StyledFormSpan = styled.span.attrs({
  'data-testid': 'error-message',
})`
  position: absolute;
  bottom: -2rem;
  left: 50%;
  transform: translateX(-50%);
  color: red;
`;

export const StyledSelect = styled(Select)`
  opacity: 0.75 !important;
  .ant-select-selector {
    border-radius: 0.8rem !important;
  }
`;
