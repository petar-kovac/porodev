import { Link } from 'react-router-dom';
import styled from 'styled-components';
import PButton from 'components/buttons/PButton';

import emailConfirmation from '../../assets/email-cropped.jpg';

const EmailConfirmed = () => {
  return (
    <StyledContainer>
      <h1>Congratulations</h1>
      <p>Your email has been verified.</p>

      <StyledConfirmationImageBox>
        <StyledConfirmationImage src={emailConfirmation} alt="" />
      </StyledConfirmationImageBox>

      <Link to="/login">
        <PButton text="Proceed to login" />
      </Link>
      <StyledBlurEffectBottom />
      <StyledBlurEffectTop />
    </StyledContainer>
  );
};

const StyledContainer = styled.div`
  width: 100vw;
  height: 100vh;
  display: flex;
  flex-direction: column;
  align-items: center;
  background-color: #f3f3f3;
  padding-top: 6rem;
`;

const StyledConfirmationImageBox = styled.div`
  display: flex;
  justify-content: center;
  padding: 5rem 0;
  width: 50rem;
  background-color: #fff;
  margin-bottom: 3rem;

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
  background: radial-gradient(circle at 50% 50%, #066bf0, #066bf0);
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
  background: radial-gradient(circle at 50% 50%, #1d6dd6, #136de2);
  opacity: 0.2;

  z-index: 1;
`;
export default EmailConfirmed;
