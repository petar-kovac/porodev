import styled from 'styled-components';

// pages

export const StyledPage = styled.div`
  padding: 20px;
`;

export const StyledHeading = styled.div`
  font-size: 24px;
  color: ${({ theme: { colors } }) => colors.primary};
  font-weight: 600;
  margin-bottom: 10px;
`;

export const StyledHeadingWrapper = styled.div`
  display: flex;
  justify-content: space-between;
`;
