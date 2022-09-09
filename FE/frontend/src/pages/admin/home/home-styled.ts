import styled from 'styled-components';

export const StyledHome = styled.div`
  margin: 3rem;
`;

export const StyledDashboardCardContainer = styled.div`
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(25rem, 16%));
  grid-column-gap: 1rem;
  grid-row-gap: 3rem;
`;

export const StyledChartsContainer = styled.div`
  margin-top: 7rem;
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(40rem, 1fr));
  grid-column-gap: 2rem;
`;
