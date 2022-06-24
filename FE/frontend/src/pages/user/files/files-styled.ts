import styled from 'styled-components';

export const PFilterWrapper = styled.div`
  width: 100%;
  display: flex;
  align-items: flex-end;
  gap: 1rem;
  padding: 2rem 0;
  justify-content: space-around;
  flex-wrap: wrap;
  background-color: #fcfcfc;
  border-radius: 3rem;
  box-shadow: 0 1px #ffffff inset, 1px 3px 8px rgba(34, 25, 25, 0.2);
  border-bottom: 2px solid #ddd;
`;

export const StyledPageWrapper = styled.div`
  display: flex;
  flex-direction: column;
`;

export const StyledContent = styled.div`
  display: flex;
`;

export const StyledStaticContent = styled.div`
  display: flex;
  flex-direction: column;
  width: 80%;
  margin: 0 auto;
  align-items: flex-start;
  gap: 5rem;
`;

export const StyledFoldersContainer = styled.div`
  display: flex;
  flex-wrap: wrap;
  margin-top: 3rem;
  gap: 2rem;
`;

export const StyledFilesWrapper = styled.div`
  padding: 0 2rem;
  display: flex;
  justify-content: center;
  gap: 2.5rem;
  flex-wrap: wrap;
`;
