import axios from 'axios';
import { useState, useEffect, FC } from 'react';
import { Link, useParams, useLocation } from 'react-router-dom';
import { useAuthStateValue } from 'context/AuthContext';
import { verifyEmail } from 'service/files/files';
import styled from 'styled-components';
import emailImage from '../../assets/email-sent.png';
import success from '../../assets/success.png';

const EmailVerify: FC = () => {
  return (
    <StyledContainer>
      <StyledRegistrationBox>
        <img src={success} alt="tick" style={{ width: '12rem' }} />
        <h1>Registration successful!</h1>
      </StyledRegistrationBox>

      <StyledVerificationBox>
        <StyledVerificationImage
          src={emailImage}
          alt="success_img"
          style={{ maxWidth: '70rem' }}
        />
        <StyledVerficationContent>
          <h1>Verify your email</h1>
          <p>
            We&apos;ve sent an email to you email address. Check your inbox in
            order to verify.
          </p>
          {/* <Link to="/login">
              <button type="button">Proceed to login</button>
            </Link> */}
        </StyledVerficationContent>
      </StyledVerificationBox>
      <StyledBlurEffectBottom />
      <StyledBlurEffectTop />
    </StyledContainer>
  );
};

const StyledContainer = styled.div`
  width: 100vw;
  height: 100vh;
  display: flex;
  align-items: center;
  flex-direction: column;
  gap: 3rem;
  background-color: #f3f3f3;
  padding-top: 6rem;
`;

const StyledRegistrationBox = styled.div`
  background-color: #fff;
  display: flex;
  align-items: center;
  padding: 4rem;
  justify-content: center;
  flex-direction: column;

  z-index: 10;

  box-shadow: 1px 3px 8px rgba(34, 25, 25, 0.2);
  border-radius: 1.5rem;

  &:hover {
    box-shadow: 2px 5px 10px rgba(34, 25, 25, 0.2);
  }
`;

const StyledVerificationBox = styled.div`
  background-color: #fff;
  display: flex;
  flex-direction: column;
  align-items: center;
  padding-bottom: 3rem;
  justify-content: center;
  text-align: center;

  position: relative;
  z-index: 10;

  box-shadow: 1px 3px 8px rgba(34, 25, 25, 0.2);
  border-radius: 1.5rem;

  &:hover {
    box-shadow: 2px 5px 10px rgba(34, 25, 25, 0.2);
  }
`;

const StyledVerificationImage = styled.img`
  max-width: 70rem;
  border-top-left-radius: 1.5rem;
  border-top-right-radius: 1.5rem;
`;

const StyledVerficationContent = styled.div`
  margin-top: 2rem;
`;

const StyledBlurEffectBottom = styled.div`
  position: absolute;
  top: 50%;
  right: -20%;
  transform: translate(-50%, 0%);
  width: 600px;
  height: 600px;
  border-radius: 50% 22% 40% 80%;
  filter: blur(100px);
  background: radial-gradient(circle at 50% 50%, #066bf0, #066bf0);
  opacity: 0.2;
`;

const StyledBlurEffectTop = styled.div`
  position: absolute;
  top: 0%;
  left: 10%;
  transform: translate(-50%, 0%);
  width: 600px;
  height: 600px;
  border-radius: 50% 22% 40% 80%;
  filter: blur(100px);
  background: radial-gradient(circle at 50% 50%, #1d6dd6, #136de2);
  opacity: 0.2;
`;

export default EmailVerify;
