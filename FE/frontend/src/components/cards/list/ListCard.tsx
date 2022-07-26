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

import useDoubleClick from 'hooks/useDoubleClick';

import { handleDownload } from 'util/helpers/files-functions';

import DownloadButton from 'components/buttons/DownloadButton';

import { downloadFile, findFiles, deleteFile } from 'service/files/files';
import { usePageContext } from 'context/PageContext';

import { formatDateListCard } from 'util/helpers/date-formaters';

import RemoveModal from '../../modal/RemoveModal';

interface IListCardProps {
  isAdmin?: boolean;
  fileId?: any;
  fileName: any;
  data: any;
  value: any;
  selected: boolean;
  userName: string;
  userLastName: string;
  onClick?: MouseEventHandler<HTMLElement>;
  onDoubleClick?: MouseEventHandler<HTMLElement>;
  setSelectedCardId: (value: number | null) => unknown;
}

const ListCard: FC<IListCardProps> = ({
  isAdmin,
  fileId,
  value,
  fileName,
  selected,
  userName,
  userLastName,
  onClick = () => undefined,
  onDoubleClick = () => undefined,
  setSelectedCardId,
}) => {
  const ref = useRef<HTMLDivElement>(null);
  useDoubleClick({ ref, onClick, onDoubleClick, stopPropagation: true });

  const { setIsSiderVisible } = usePageContext();

  const [isRemoveModalVisible, setIsRemoveModalVisible] =
    useState<boolean>(false);

  // const handleDownload = async () => {
  //   const res = await downloadFile(fileId as string);
  //   const url = window.URL.createObjectURL(new Blob([res.data]));

  //   const link = document.createElement('a');
  //   link.href = url;
  //   link.setAttribute('download', `${fileName}`);
  //   document.body.appendChild(link);
  //   link.click();

  //   link.parentNode?.removeChild(link);
  // };

  // const handleDelete = async () => {
  //   await deleteFile(fileId as string);
  //   const listCard = document.getElementById('remove-id');
  //   listCard?.parentNode?.removeChild(listCard);

  //   setIsRemoveModalVisible(false);
  // };

  const handleCancel = () => {
    setIsRemoveModalVisible(false);
  };

  const formattedDate = formatDateListCard(value.uploadDateTime);

  return (
    <>
      <StyledListCardContainer>
        <StyledListCard ref={ref} hoverable selected={selected} id="remove-id">
          <StyledDescription>
            <StyledHeading>
              <h3>{value.filename}</h3>
              {isAdmin && (
                <span>
                  by {userName} {userLastName}
                </span>
              )}
            </StyledHeading>
            <StyledDescriptionUploadDetails>
              <span>{formattedDate}</span>
            </StyledDescriptionUploadDetails>
            <StyledDescriptionButtons>
              <StyledFilesButton
                onClickCapture={(e) => {
                  e.stopPropagation();
                  setIsRemoveModalVisible(true);
                  setIsSiderVisible(false);
                  setSelectedCardId(value.fileId);
                }}
              >
                Remove file
              </StyledFilesButton>
              <a
                type="button"
                onClickCapture={(e) => {
                  e.stopPropagation();
                  handleDownload(fileId, fileName);
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
      <RemoveModal
        fileId={fileId}
        isRemoveModalVisible={isRemoveModalVisible}
        setIsRemoveModalVisible={setIsRemoveModalVisible}
        // handleDelete={handleDelete}
        handleCancel={handleCancel}
        fileName={fileName}
      />
    </>
  );
};

export const StyledListCard = styled(Card).attrs({
  'data-testid': 'list-card',
})<{ selected: boolean; ref: RefObject<HTMLDivElement | null> }>`
  box-shadow: 0 1px #ffffff inset, 1px 3px 8px rgba(34, 25, 25, 0.2);
  /* height: 7.5rem; */
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

const StyledHeading = styled.div`
  flex-basis: 40%;
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
