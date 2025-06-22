export const getMarkerPopupHTML = ({ title, description, image }) => {
    return `
    <div style="
      background: white;
      border-radius: 8px;
      box-shadow: 0 4px 12px rgba(0, 0, 0, 0.15);
      overflow: hidden;
      width: 280px;
      font-family: Arial, sans-serif;
      position: relative;
    ">
      <!-- Кнопка закрытия -->
      <div style="
        position: absolute;
        top: 8px;
        right: 8px;
        width: 24px;
        height: 24px;
        background: rgba(0, 0, 0, 0.5);
        border-radius: 50%;
        display: flex;
        align-items: center;
        justify-content: center;
        cursor: pointer;
        z-index: 10;
      " class="popup-close-btn">
        <svg width="12" height="12" viewBox="0 0 12 12" fill="none">
          <path d="M1 1L11 11M1 11L11 1" stroke="white" stroke-width="2" stroke-linecap="round"/>
        </svg>
      </div>

      ${image ? `
        <div style="
          height: 160px;
          background: #f5f5f5;
          overflow: hidden;
          display: flex;
          align-items: center;
          justify-content: center;
        ">
          <img src="data:image/jpeg;base64,${image}" 
               style="
                 max-width: 100%;
                 max-height: 100%;
                 object-fit: cover;
               " 
               alt="${title}"/>
        </div>
      ` : ''}
      
      <div style="padding: 16px;">
        <h3 style="
          margin: 0 0 8px 0;
          font-size: 18px;
          color: #333;
          font-weight: 600;
        ">
          ${title}
        </h3>
        
        ${description ? `
          <p style="
            margin: 0 0 16px 0;
            font-size: 14px;
            color: #666;
            line-height: 1.4;
          ">
            ${description}
          </p>
        ` : ''}
        
        <button class="details-btn" style="
          width: 100%;
          padding: 10px 0;
          background: #3b7eff;
          color: white;
          border: none;
          border-radius: 4px;
          font-size: 14px;
          font-weight: 500;
          cursor: pointer;
          transition: background 0.2s;
        ">
          Подробнее
        </button>
      </div>
    </div>
  `;
};