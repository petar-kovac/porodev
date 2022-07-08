import { Card, Button } from 'antd';
import styled from 'styled-components';
import {
  FC,
  useRef,
  MouseEventHandler,
  RefObject,
  useState,
  useEffect,
  useCallback,
} from 'react';

import theme from 'theme/theme';

import api from 'service/base';

import useDoubleClick from 'hooks/useDoubleClick';

import DownloadButton from 'components/buttons/DownloadButton';

import { downloadFile, findFiles, deleteFile } from 'service/files/files';

interface IListCardProps {
  fileId: any;
  fileName: any;
  data: any;
  value: any;
  selected: boolean;
  onClick?: MouseEventHandler<HTMLElement>;
  onDoubleClick?: MouseEventHandler<HTMLElement>;
  setSelectedCardId: (value: number | null) => unknown;
  setIsSiderVisible: (value: boolean) => unknown;
}

const ListCard: FC<IListCardProps> = ({
  fileId,
  fileName,
  data,
  value,
  selected,
  onClick = () => undefined,
  onDoubleClick = () => undefined,
  setSelectedCardId,
  setIsSiderVisible,
}) => {
  const ref = useRef<HTMLDivElement>(null);
  useDoubleClick({ ref, onClick, onDoubleClick, stopPropagation: true });

  const handleDownload = async () => {
    const res = await downloadFile(fileId as string);
    const url = window.URL.createObjectURL(new Blob([res.data]));

    const link = document.createElement('a');
    link.href = url;
    link.setAttribute('download', `${value.fileName}`);
    document.body.appendChild(link);
    link.click();

    link.parentNode?.removeChild(link);
  };

  const handleDelete = async () => {
    await deleteFile(fileId as string);
    const listCard = document.getElementById('remove-id');
    listCard?.parentNode?.removeChild(listCard);
  };

  return (
    <StyledListCardContainer>
      <StyledListCard ref={ref} hoverable selected={selected} id="remove-id">
        <StyledDescription>
          <div>
            <h3>{value.fileName}</h3>
          </div>
          <StyledDescriptionUploadDetails>
            <h4>Uploaded:</h4>
            <span>{value.uploadTime}</span>
          </StyledDescriptionUploadDetails>
          <StyledDescriptionButtons>
            <StyledFilesButton
              onClickCapture={(e) => {
                console.log(e);
                handleDelete();
              }}
            >
              Remove file
            </StyledFilesButton>
            <a
              type="button"
              onClickCapture={(e) => {
                e.stopPropagation();
                handleDownload();
                setIsSiderVisible(false);
                setSelectedCardId(value.fileId);
              }}
            >
              <DownloadButton />
            </a>
          </StyledDescriptionButtons>
        </StyledDescription>
      </StyledListCard>
    </StyledListCardContainer>
  );
};

export const StyledListCard = styled(Card).attrs({
  'data-testid': 'list-card',
})<{ selected: boolean; ref: RefObject<HTMLDivElement | null> }>`
  box-shadow: 0 1px #ffffff inset, 1px 3px 8px rgba(34, 25, 25, 0.2);
  height: 7.5rem;
  border-radius: 1.5rem;
  overflow: hidden;
  border: 2px solid
    ${({ selected }) => (selected ? `${theme.colors.selected}` : '#fff')};
  background-color: ${({ selected }) =>
    selected ? `${theme.colors.selectedBackground}` : '#fff'};

  &:hover,
  &:active,
  &:focus {
    border: 2px solid
      ${({ selected }) => (selected ? `${theme.colors.selected}` : '#fff')};
  }
  .ant-card-cover {
    height: 14rem;
    width: 24rem;
    overflow: hidden;
    border-top-left-radius: 1.5rem;
    border-top-right-radius: 1.5rem;
  }

  .ant-card-body {
    padding: 1rem 2rem;
  }
`;

const StyledListCardContainer = styled.div`
  width: 100%;
`;

const StyledFilesButton = styled(Button)`
  border-radius: 0.8rem;
`;

const StyledDescription = styled.div`
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding-top: 0.5rem;
`;

const StyledDescriptionButtons = styled.div`
  display: flex;
  gap: 1rem;
`;

const StyledDescriptionUploadDetails = styled.div`
  text-align: center;
`;

export default ListCard;
