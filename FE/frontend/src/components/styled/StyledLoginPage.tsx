import styled from 'styled-components';
import landingImage from '../../assets/landing.jpg';

const StyledPage = styled.div`
  display: flex;
  flex-direction: column;
  justify-content: center;
  height: 100vh;
  background-image: url(${landingImage});
  background-size: cover;
  background-position: center;
  background-repeat: no-repeat;
`;

const StyledFormWrapper = styled.div`
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

export { StyledPage, StyledFormWrapper };
