import styled from 'styled-components';

export const StyledPage = styled.div`
  padding: 20px;
`;
export const StyledHeading = styled.div`
  font-size: 24px;
  color: ${({ theme: { colors } }) => colors.primary};
  font-weight: 600;
`;
export const StyledHeadingWrapper = styled.div`
  display: flex;
  justify-content: space-between;
`;
