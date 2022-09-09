import { FC } from 'react';
import { Link } from 'react-router-dom';

import styled from 'styled-components';

import PButton from 'components/buttons/PButton';

import emailVerified from '../../assets/error-verified.png';

const EmailVerified: FC = () => {
  return (
    <StyledVerifiedContainer>
      <StyledVerifiedContent>
        <h1>You are already verified.</h1>
        <p>Click on the button below to login</p>
        <StyledConfirmationImageBox>
          <StyledConfirmationImage src={emailVerified} alt="error-verified" />
        </StyledConfirmationImageBox>
      </StyledVerifiedContent>
      <Link to="/login">
        <PButton text="Proceed to login" />
      </Link>
      <StyledBlurEffectBottom />
      <StyledBlurEffectTop />
    </StyledVerifiedContainer>
  );
};

const StyledVerifiedContainer = styled.div`
  width: 100vw;
  height: 100vh;
  display: flex;
  flex-direction: column;
  align-items: center;
  padding-top: 6rem;
  background-color: #f3f3f3;

  text-align: center;
`;

const StyledVerifiedContent = styled.div``;

const StyledConfirmationImageBox = styled.div`
  display: flex;
  justify-content: center;
  padding: 5rem 0;
  width: 50rem;
  background-color: #fff;
  margin: 3rem 0;

  box-shadow: 1px 3px 8px rgba(34, 25, 25, 0.2);
  border-radius: 1.5rem;

  position: relative;

  &:hover {
    box-shadow: 2px 5px 10px rgba(34, 25, 25, 0.2);
  }

  z-index: 5;
`;

const StyledConfirmationImage = styled.img`
  max-width: 40rem;
`;

const StyledBlurEffectBottom = styled.div`
  position: absolute;
  top: -10%;
  left: -10%;
  width: 600px;
  height: 600px;
  border-radius: 50% 22% 40% 80%;
  filter: blur(100px);
  background: radial-gradient(circle at 50% 50%, #ff1f1f, #ff1616);
  opacity: 0.2;

  z-index: 1;
`;

const StyledBlurEffectTop = styled.div`
  position: absolute;
  top: 35%;
  left: 50%;

  width: 600px;
  height: 600px;
  border-radius: 50% 22% 40% 80%;
  filter: blur(100px);
  background: radial-gradient(circle at 50% 50%, #ff1f1f, #ff1616);
  opacity: 0.2;

  z-index: 1;
`;

export default EmailVerified;
