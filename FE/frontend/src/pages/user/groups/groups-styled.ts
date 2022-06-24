import { Content } from 'antd/lib/layout/layout';
import styled from 'styled-components';

export const StyledContent = styled(Content)`
  display: flex;
  flex-direction: column;
  justify-content: flex-end;
`;

export const StyledFilterWrapper = styled.div`
  display: flex;
  flex: 1;
  align-items: flex-end;
  justify-content: flex-end;
`;

export const StyledHeadingText = styled.div`
  font-size: 24px;
  color: ${({ theme: { colors } }) => colors.primary};
  font-weight: 600;
`;

export const StyledHeadingWrapper = styled.div`
  width: 100%;
  padding: 20px;
  align-items: center;
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

export const StyledFilesWrapper = styled.div`
  padding: 0 2rem;
  display: flex;
  justify-content: center;
  gap: 2.5rem;
  width: 90%; // fix not to break card until grid has been implemented
  flex-wrap: wrap;
`;
