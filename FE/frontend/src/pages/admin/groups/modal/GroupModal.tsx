import { Modal } from 'antd';
import { usePageContext } from 'context/PageContext';
import { Dispatch, FC, ReactNode, SetStateAction } from 'react';
import styled from 'styled-components';
import { IFilesCard } from 'types/card-data';

interface IPModalProps {
  title?: string;
  content?: ReactNode;
  onOk?: any;
  onCancel?: any;
  setCardData?: Dispatch<SetStateAction<IFilesCard | null>>;
  cardData?: IFilesCard | null;
}

const GroupModal: FC<IPModalProps> = ({
  title,
  content,
  setCardData,
  onOk,
  onCancel,
}) => {
  const { isModalVisible, setIsModalVisible } = usePageContext();

  const handleOk = () => {
    setIsModalVisible(false);
  };

  const handleCancel = () => {
    setIsModalVisible(false);
  };

  return (
    <>
      <StyledFilesModal
        title={title}
        visible={isModalVisible}
        onOk={handleOk}
        onCancel={handleCancel}
      >
        <div>Group modal</div>
      </StyledFilesModal>
    </>
  );
};

const StyledFilesModal = styled(Modal).attrs({
  'data-testid': 'modal',
})`
  .ant-modal-header {
    border-radius: 1.2rem;
    background-color: rgba(220, 220, 220, 0.1);
    border: 1px solid #fff;
    border-bottom: 1px solid #eee;
  }

  .ant-modal-title {
    color: #555;
    letter-spacing: 0.5px;
    font-size: 1.8rem;
  }

  .ant-modal-content {
    border-radius: 1.2rem;
    box-shadow: 1px 3px 4px rgba(255, 255, 255, 0.4);
  }

  .ant-modal-footer {
    padding: 1.4rem 2.2rem;
    border-radius: 1.6rem;

    button {
      border-radius: 0.8rem;
    }

    .footer-content {
      display: flex;
      justify-content: space-between;
      align-items: center;
    }
  }
`;

export default GroupModal;
